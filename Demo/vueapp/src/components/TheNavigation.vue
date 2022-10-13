<template>
  <div id="nav">
    <router-link id="logo" to="/">Locations App</router-link> |
    <router-link
      v-for="location in locations"
      :key="location.id"
      :to="{
        name: 'location.show',
        params: { id: location.id, slug: location.name },
      }"
    >
      {{ location.name }} |
    </router-link>
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

<style lang="css">
#nav .vue-active-link {
  color: red;
  border-bottom: 2px solid orange;
}
</style>
