export default class TrailsApi {


  static create() {
    return fetch('/api/trails', {
      method: 'POST'
    }).then(result => result.json()
    ).catch(error => {
      console.log(error);
    });
  }

  static edit(trailId) {
    return fetch(`/api/trails/${trailId}/edit`, {
      method: 'POST'
    }).then(result => result.json()
    ).catch(error => {
      console.log(error);
      alert(error);
    });
  }

  static getEdit(editId) {
    return fetch(`/api/trails/edit/${editId}`, {
      method: 'GET'
    }).then(result => result.json()
    ).catch(error => {
      console.log(error);
      alert(error);
    });
  }

  static save(trailId, editId, trail) {
    return fetch(`/api/trails/${trailId}/edit/${editId}`, {
      method: 'POST',
      body: JSON.stringify(trail),
      headers: {
        "Content-Type": "application/json"
      }
    }).then(result => result.ok
    ).catch(error => {
      console.log(error);
    });
  }

  static commit(trailId, editId) {
    return fetch(`/api/trails/${trailId}/commit/${editId}`, {
      method: 'POST'
    }).then(result => result.ok
    ).catch(error => {
      console.log(error);
    });
  }

  static getAll() {
    return fetch('/api/trails', {
      method: 'GET',
    })
    .then(result => result.json());
  }
}