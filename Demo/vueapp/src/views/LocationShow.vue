<template>
  <section class="location">
    <h1>{{ location.name }}</h1>
    <dic class="locatio-details">
      <img :src="'/images/' + location.image" :alt="location.name" />
    </dic>
  </section>
</template>
<script>
import sourceData from "@/data.json";

export default {
  computed: {
    locationId() {
      return parseInt(this.$route.params.id);
    },
    location() {
      return sourceData.locations.find(
        (location) => location.id == this.locationId
      );
    },
  },
};
</script>
