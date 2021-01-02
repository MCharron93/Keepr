<template>
  <div class="keep card bg-light text-white align-text-bottom">
    <img :src="keepProp.img" alt="" class="card-img">
    <div class="card-img-overlay row justify-content-between align-items-end">
      <h3 class="card-title">
        {{ keepProp.name }}
      </h3>
      <div class="text-info" @click="seeProfilePage(keepProp.creatorId)">
        UserIcon here
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
export default {
  name: 'Keep',
  props: {
    keepProp: Object
  },
  setup(props) {
    return {
      keep: computed(() => props.keepProp)
    }
  }
}
</script>

<style>

</style>
