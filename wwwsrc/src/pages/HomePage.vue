<template>
  <div class="home grid justify-content-around">
    <keeps-component v-for="k in keeps" :key="k.id" :keep-prop="k" />
  </div>
</template>

<script>
import { keepsService } from '../services/KeepsService'
import { computed, onMounted } from 'vue'
import KeepsComponent from '../components/KeepsComponent.vue'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
export default {
  name: 'Home',
  components: { KeepsComponent },
  setup() {
    onMounted(async() => {
      await keepsService.getAllKeeps()
      await profileService.getProfile()
    })
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.allKeeps)
      // myVaults: computed(() => AppState.viewingVaults)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
