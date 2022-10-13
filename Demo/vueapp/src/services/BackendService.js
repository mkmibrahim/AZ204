import axios from "axios";

export default {
  getWeatherFunction(locationName) {
    return axios.get(
      process.env.VUE_APP_NET_API + "/api/City/Get?cityName=" + locationName
    );
  },
  getCitiesFunction() {
    return axios.get(process.env.VUE_APP_NET_API + "/api/City/GetCities");
  },
};
