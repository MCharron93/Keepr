<template>
  <div class="vault">
    {{ vault }}
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    onMounted(() => {
      vaultsService.getKeepsByVaultId(route.params.id)
    })
    return {
      vault: computed(() => AppState.oneVault),
      keeps: computed(() => AppState.viewingKeeps)
    }
  }
}
</script>

<style>

</style>
