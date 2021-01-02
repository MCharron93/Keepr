<template>
  <div class="about text-center">
    <h1>Welcome {{ profile.name }}</h1>
    <img class="rounded" :src="profile.picture" alt="" />
    <p>{{ profile.email }}</p>
    <div class="row">
      <keeps-component v-for="k in mykeeps" :keep-prop="k" :key="k.id" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import KeepsComponent from '../components/KeepsComponent.vue'
import { keepsService } from '../services/KeepsService'
export default {
  components: { KeepsComponent },
  name: 'Profile',
  setup() {
    onMounted(() => {
      keepsService.getMyKeeps()
    })
    return {
      profile: computed(() => AppState.profile)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
