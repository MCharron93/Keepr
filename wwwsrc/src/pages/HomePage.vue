<template>
  <div class="home container">
    <div class="row">
      <div class="col-12">
        <div class="card-columns">
          <keeps-component v-for="k in keeps" :key="k.id" :keep-prop="k" class="card-container mx-2" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { keepsService } from '../services/KeepsService'
import { computed, onMounted } from 'vue'
import KeepsComponent from '../components/KeepsComponent.vue'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  components: { KeepsComponent },
  setup() {
    onMounted(async() => {
      await keepsService.getAllKeeps()
    })
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.allKeeps)
    }
  }
}
</script>

<style scoped lang="scss">

</style>
