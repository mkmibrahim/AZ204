<template>
  <section v-if="location" class="location">
    <h1>{{ location.name }}</h1>
    <dic class="location-details">
      <img :src="'/images/' + location.image" :alt="location.name" />
    </dic>
  </section>
</template>
<script>
//import sourceData from "@/data.json";

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
  },
  async created() {
    const response = await fetch(
      //"https://travel-dummy-api.netlify.app/${this.$route.params.slug}"
      "https://travel-dummy-api.netlify.app/brazil"
    );
    this.location = await response.json();
    this.$watch(
      () => this.$route.params,
      async () => {
        const response = await fetch(
          //"https://travel-dummy-api.netlify.app/${this.$route.params.slug}"
          "https://travel-dummy-api.netlify.app/brazil"
        );
        this.location = await response.json();
      }
    );
  },
};
</script>
