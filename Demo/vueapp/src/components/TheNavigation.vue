<template>
  <div id="nav">
    <router-link id="logo" to="/">Locations App</router-link> |
    <router-link
      v-for="location in locations"
      :key="location.id"
      :to="{
        name: 'location.show',
        params: { id: location.id, slug: location.slug },
      }"
    >
      {{ location.name }} |
    </router-link>
  </div>
</template>

<script>
import sourceData from "@/data.json";
export default {
  data() {
    return {
      locations: sourceData.locations,
    };
  },
};
</script>

<style lang="css">
#nav .vue-active-link {
  color: red;
  border-bottom: 2px solid orange;
}
</style>
