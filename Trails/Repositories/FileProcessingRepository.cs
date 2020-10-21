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
      var existingTransform = _context.AppliedFileTransforms.Where(aft => 
        aft.fileId == transform.fileId && aft.transformName == transform.transform
      ).FirstOrDefault();
      if (existingTransform != null) {
        existingTransform.appliedTransforms = transforms.ToArray();
        existingTransform.transformJobId = transform.id;
        _context.AppliedFileTransforms.Update(existingTransform);
      } else {
        _context.AppliedFileTransforms.Add(new AppliedFileTransforms() {
          appliedTransforms = transforms.ToArray(),
          fileId = transform.fileId,
          transformJobId = transform.id,
          transformName = transform.transform
        });
      }
      _context.SaveChanges();
    }

    public void SetFileStatus(int fileId, FileStatus status)
    {
      var entity = _context.FileTransforms.Find(fileId);
      entity.status = status;
      _context.SaveChanges();
    }

    public FileTransform GetFile(int fileId) {
      var file = _context.FileTransforms.Find(fileId);
      if (file == null) return null;
      var transformJobs = _context.Transforms.Where(t => t.fileId == fileId);
      var status = FileStatus.UPLOADING;
      if (transformJobs.Count() > 0 && transformJobs.All(t => t.status == FileStatus.DONE)) {
        status = FileStatus.DONE;
      } else if(transformJobs.Any(t => t.status == FileStatus.ERRORED)) {
        status = FileStatus.ERRORED;
      } else if(transformJobs.Any(t => t.status == FileStatus.QUEUED)) {
        status = FileStatus.QUEUED;
      }
      file.status = status;
      return file;
    }
  }
}