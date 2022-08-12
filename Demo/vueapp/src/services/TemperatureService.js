import axios from "axios";

const client = axios.create({
  baseURL: "https://localhost:5001",
  json: true,
});

export default {
  async execute(method, resource, data) {
    return client({
      method,
      url: resource,
      data,
      headers: {},
    }).then((req) => {
      return req.data;
    });
  },
  getTemperatureFunction(locationName) {
    const path = "/api/weather/Get/" + locationName;
    const response = this.execute("get", path);
    return response;
  },
};
