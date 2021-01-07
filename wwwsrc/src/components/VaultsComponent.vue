<template>
  <div class="vaults-component my-3" @click="viewVault">
    <div class="card bg-light align-text-bottom">
      <img :src="vaultProp.img" alt="" class="card-img">
      <div class="card-img-overlay row justify-content-between align-items-end text-white">
        <h3 class="card-title">
          {{ vaultProp.name }}
        </h3>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
// import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultsComponent',
  props: {
    vaultProp: Object
  },
  setup(props) {
    const router = useRouter()
    return {
      profile: computed(() => AppState.profile),
      vaults: computed(() => props.vaultProp),
      viewVault() {
        router.push({ name: 'Vault', params: { id: props.vaultProp.id } })
      }
    }
  }

}
</script>

<style>

</style>
