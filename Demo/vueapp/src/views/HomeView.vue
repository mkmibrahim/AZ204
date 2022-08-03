<template>
  <div class="home">
    <h1>Locations</h1>
    <div class="locations">
      <router-link
        v-for="location in locations"
        :key="location.id"
        :to="location.slug"
      >
        <h2>{{ location.name }}</h2>
        <img :src="'/images/' + location.image" :alt="location.name" />
      </router-link>
    </div>
  </div>
</template>
<script>
import soureData from "@/data.json";
export default {
  data() {
    return {
      locations: soureData.locations,
    };
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
