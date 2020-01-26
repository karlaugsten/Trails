export default class TrailsApi {

  static checkStatus(response) {
    if(response.status >= 200 && response.status <= 300) {
      return response.json();
    } else {
      return response.text().then(text => {
        let error = new Error(text);
        error.response = response;
        return Promise.reject(error);
      })
    }
  }

  static create() {
    return fetch('/api/trails', {
      method: 'POST',
      headers: {
        "Authorization": "Bearer " + localStorage.getItem("token"),
      },
    }).then(TrailsApi.checkStatus)
  }

  static edit(trailId) {
    return fetch(`/api/trails/${trailId}/edit`, {
      method: 'POST',
      headers: {
        "Authorization": "Bearer " + localStorage.getItem("token"),
      },
    }).then(TrailsApi.checkStatus)
  }

  static getEdit(editId) {
    return fetch(`/api/trails/edit/${editId}`, {
      method: 'GET',
      headers: {
        "Authorization": "Bearer " + localStorage.getItem("token"),
      },
    }).then(TrailsApi.checkStatus)
  }

  static save(trailId, editId, trail) {
    return fetch(`/api/trails/${trailId}/edit/${editId}`, {
      method: 'POST',
      body: JSON.stringify(trail),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("token"),
      }
    }).then(TrailsApi.checkStatus)
  }

  static commit(trailId, editId) {
    return fetch(`/api/trails/${trailId}/commit/${editId}`, {
      method: 'POST',
      headers: {
        "Authorization": "Bearer " + localStorage.getItem("token"),
      }
    }).then(TrailsApi.checkStatus)
  }

  static getAll() {
    return fetch('/api/trails', {
      method: 'GET',
      headers: {
        "Authorization": "Bearer " + localStorage.getItem("token"),
      }
    })
    .then(TrailsApi.checkStatus);
  }

  static heartTrail(trailId) {
    return fetch(`/api/trails/${trailId}/heart`, {
      method: 'POST',
      headers: {
        "Authorization": "Bearer " + localStorage.getItem("token"),
      }
    })
    .then(TrailsApi.checkStatus);
  }
}