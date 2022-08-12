<template>
  <section v-if="location" class="location">
    <h1>{{ location.name }}</h1>
    <div class="locatio-details">
      <img :src="'/images/' + location.image" :alt="location.name" />
      <p>Temperature: {{ location.temperature }}</p>
      <p>Humidity: {{ location.humidity }}</p>
    </div>
  </section>
</template>
<script>
import api from "@/services/TemperatureService.js";
export default {
  data() {
    return {
      location: null,
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
    //this.$watch(() => this.$route.params, this.initData);
  },
  methods: {
    async initData() {
      const response = await api.getTemperatureFunction(
        this.$route.params.slug
      );
      this.location = response;
    },
  },
};
</script>
