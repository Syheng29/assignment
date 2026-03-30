import axios from "axios";

// const API_URL = "http://localhost:8080/api/users"
const API_URL = "https://localhost:44303/api/notes";

export default {
  getAll() {
    return axios.get(API_URL);
  },
  create(data) {
    return axios.post(API_URL, data);
  },
  update(id, data) {
    return axios.put(`${API_URL}/${id}`, data);
  },
  delete(id) {
    return axios.delete(`${API_URL}/${id}`);
  },
};
