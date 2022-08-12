<template>
  <section v-if="location" class="location">
    <h1>{{ location.name }}</h1>
    <div class="locatio-details">
      <img :src="'/images/' + location.image" :alt="location.name" />
    </div>
  </section>
</template>
<script>
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
    //const response = await fetch("https://travel-dummy-api.netlify.app/brazil");
    const response = await fetch("/api/weather");
    this.location = await response.json();
  },
};
</script>
