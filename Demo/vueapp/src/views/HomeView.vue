<template>
  <div class="home">
    <h1>Locations</h1>
    <div class="locations">
      <router-link
        v-for="location in locations"
        :key="location.id"
        :to="{
          name: 'location.show',
          params: { id: location.id, slug: location.name },
        }"
      >
        <h2>{{ location.name }}</h2>
        <img :src="location.image" :alt="location.name" />
      </router-link>
    </div>
  </div>
</template>
<script>
import api from "@/services/BackendService.js";
export default {
  data() {
    return {
      locations: null,
    };
  },
  async created() {
    this.initData();
  },
  methods: {
    async initData() {
      const response = await api.getCitiesFunction();
      console.log("Response is %o", response);
      this.locations = response.data;
    },
  },
};
</script>

<style>
img {
  max-width: 230px;
}
.locations {
  display: flex;
  justify-content: space-between;
}
</style>
