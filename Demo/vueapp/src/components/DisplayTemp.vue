<template>
  <div class="display-temp">
    <h4>Temperature</h4>
    <div class="btn">
      <button @click="getTemperature()" type="button" class="btn bten-warning">
        Get Temperature
      </button>
      <h3>current temperature is {{ GetTemperatureService() }} .</h3>
    </div>
  </div>
</template>

<script>
// export default {
//   //name: DisplayTemp,
//   props: ["numberofOfUsers"],
//   methods: {
//     getTemperature() {
//       this.$emit("getTemperature");
//     },
//   },
// };

//import { getTemperatureFunction } from "../services/TemperatureService.js";

export default {
  name: "TemperatureService",
  methods: {
    GetTemperatureService() {
      return 1; //+ this.$emit("getTemperatureFunction");
      //return getTemperatureFunction();
    },
  },
};
</script>
