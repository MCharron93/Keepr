<template>
  <div class="vault">
    Here is a vault page
  </div>
</template>

<script>
import { onMounted } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
// import { computed } from 'vue'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    onMounted(() => {
      vaultsService.getKeepsByVaultId(route.params.id)
    })
    return {
      // keeps: computed(() => )
    }
  }
}
</script>

<style>

</style>
