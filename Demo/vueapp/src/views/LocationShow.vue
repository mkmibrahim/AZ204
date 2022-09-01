<template>
  <section v-if="location" class="location">
    <h1>{{ location.name }}</h1>
    <div class="locatio-details">
      <!-- <img :src="'/images/' + location.image" :alt="location.name" /> -->
      <img :src="location.image" :alt="location.name" />
      <p>Temperature: {{ location.temperature }}</p>
      <p>Humidity: {{ location.humidity }}</p>
    </div>
    <GoBack />
  </section>
  <!-- <div id="app">
    {{ info }}
  </div> -->
</template>
<script>
//import axios from "axios";
import api from "@/services/TemperatureService.js";
import GoBack from "@/components/GoBack.vue";
export default {
  components: { GoBack },
  data() {
    return {
      location: null,
      info: null,
    };
  },
  computed: {
    locationId() {
      return parseInt(this.$route.params.id);
    },
    locationSlug() {
      return this.$route.params.slug;
    },
  },
  async created() {
    this.initData();
  },
  methods: {
    async initData() {
      const response = await api.getTemperatureFunction(
        this.$route.params.slug
      );
      console.log("Response is %o", response);
      this.location = response.data;
    },
  },
  // mounted() {
  //   axios
  //     .get("https://api.coindesk.com/v1/bpi/currentprice.json")
  //     .then((response) => {
  //       this.info = response;
  //     });
  // },
};
</script>
