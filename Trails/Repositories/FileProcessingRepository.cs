using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Trails.FileProcessing;
using Trails.FileProcessing.Models;
using Trails.Models;

namespace Trails.Repositories
{

  public class FileProcessingRepository : IFileProcessingRepository
  {
    private FileProcessingDbContext _context;

    public FileProcessingRepository(FileProcessingDbContext context)
    {
      _context = context;
    }

    public Dictionary<string, List<string>> GetAppliedTransforms(int fileId)
    {
      var appliedTransforms = _context.AppliedFileTransforms.Where(applied => applied.fileId == fileId);
      return appliedTransforms.ToDictionary(k => k.transformName, k => k.appliedTransforms.ToList());
    }

    public List<FileTransform> GetFiles()
    {
      return _context.FileTransforms.OrderBy(f => f.startTime).ToList();
    }

    public FileTransform SaveFile(FileTransform file)
    {
      var entity = _context.FileTransforms.Add(file);
      _context.SaveChanges();
      return entity.Entity;
    }

    public TransformJob SaveTransform(TransformJob transform)
    {
      EntityEntry<TransformJob> entity;
      if (_context.Transforms.Contains(transform)) {
        entity = _context.Transforms.Update(transform);
      }
      else {
        entity = _context.Transforms.Add(transform);
      }
      _context.SaveChanges();
      return entity.Entity;
    }

    public void SaveTransforms(TransformJob transform, IEnumerable<string> transforms)
    {
      if (_context.Transforms.Contains(transform)) {
        _context.Transforms.Update(transform);
      } else {
        _context.Transforms.Add(transform);
      }
      _context.AppliedFileTransforms.Add(new AppliedFileTransforms() {
        appliedTransforms = transforms.ToArray(),
        fileId = transform.fileId,
        transformJobId = transform.id,
        transformName = transform.transform
      });
      _context.SaveChanges();
    }

    public void SetFileStatus(int fileId, FileStatus status)
    {
      var entity = _context.FileTransforms.Find(fileId);
      entity.status = status;
      _context.SaveChanges();
    }
  }
}