import axios from "axios";

// const client = axios.create({
//   //baseURL: "https://localhost:5001",
//   baseURL: process.env.NET_API,
//   json: true,
// });

export default {
  // async execute(method, resource, data) {
  //   return client({
  //     method,
  //     url: resource,
  //     data,
  //     headers: {},
  //   }).then((req) => {
  //     return req.data;
  //   });
  // },
  getTemperatureFunction(locationName) {
    //console.log("VUE_APP_NET_API is " + process.env.VUE_APP_NET_API);
    return axios.get(
      process.env.VUE_APP_NET_API + "/api/city/Get/" + locationName
    );
  },
};
