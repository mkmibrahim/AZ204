<template>
  <div class="Amsterdam">
    <h1>Amsterdam</h1>
    <h3>Current temperature: 27 &#176;</h3>
    <div class="Temperature Dynamic">
      <DisplayTemp />
    </div>
  </div>
</template>

<script>
import DisplayTemp from "../components/DisplayTemp.vue";
export default { components: { DisplayTemp } };
</script>
