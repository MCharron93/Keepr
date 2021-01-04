<template>
  <div class="profile container-fluid">
    <div class="row text-left d-flex p-5">
      <img class="img-fluid mw-100" :src="profile.picture" alt="" />
      <div class="offset-1">
        <h1>
          {{ profile.name }}
        </h1>
        <h4>
          Vaults: {{ profile.vaults }}
        </h4>
        <h4>
          Keeps: {{ profile.keeps }}
        </h4>
      </div>
    </div>
    <div class="row justify-content-between p-5">
      <h2 class="col-12">
        Vaults
      </h2>
      <!-- Insert vault component here -->
    </div>
    <div class="row justify-content-between p-5">
      <h2 class="col-12">
        Keeps
      </h2>
      <keeps-component v-for="k in keeps" :keep-prop="k" :key="k.id" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import KeepsComponent from '../components/KeepsComponent.vue'
import { profileService } from '../services/ProfileService'
export default {
  components: { KeepsComponent },
  name: 'Profile',
  setup() {
    onMounted(async() => {
      await profileService.getProfile()
      await profileService.getKeepsByProfileId(AppState.profile.id)
    })
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.viewingKeeps)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
