// import Vue from "vue";
// import axios from "axios";

// const client = axios.create({
//   baseURL: "http://localhost:5000/api",
// });

// export default {
//   async execute(method, resource, data) {
//     const accessToken = await Vue.prototype.$auth.getAccessToken();
//     return client({
//       method,
//       url: resource,
//       data,
//       headers: {
//         Authorization: Bearer "${accessToken}" /* eslint-disable-line */
//       },
//     });
//   },
//   getTemperature() {
//     return this.execute("get", "Temperature");
//   },
// };
