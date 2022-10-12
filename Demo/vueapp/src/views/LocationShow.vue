<template>
  <section v-if="location" class="location">
    <h1>{{ location.name }}</h1>
    <div class="location-details">
      <img :src="location.image" :alt="location.name" />
      <p>Temperature: {{ location.weather.temperature }}</p>
      <p>Humidity: {{ location.weather.humidity }}</p>
      <table border="1">
        <thead>
          <tr>
            <th>Time</th>
            <th>Temperature</th>
            <th>Humidity</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="weatherData in location.weather.history"
            :key="weatherData.time"
          >
            <td>{{ weatherData.time }}</td>
            <td>{{ weatherData.temperature }}</td>
            <td>{{ weatherData.humidity }}</td>
          </tr>
        </tbody>
      </table>
      <p>test test</p>
    </div>
    <GoBack />
  </section>
</template>
<script>
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
};
</script>

<style>
table {
  border-collapse: collapse;
  margin: auto;
  padding: 0;
  width: calc(100% - 200px);
}
</style>
