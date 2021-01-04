<template>
  <div class="vault">
    <h2>
      {{ vault.name }}
    </h2>
    <keeps-component v-for="k in keeps" :keep-prop="k" :key="k.id" />
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import KeepsComponent from '../components/KeepsComponent.vue'
export default {
  components: { KeepsComponent },
  name: 'Vault',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultsService.viewVault(route.params.id)
      await vaultsService.getKeepsByVaultId(route.params.id)
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
