<template>
  <div class="vaults-component card col-3" @click="viewVault">
    <div class="bg-light align-text-bottom">
      <!-- <img :src="vaultProp.img" alt="" class="card-img"> -->
      <div class="row justify-content-between align-items-end text-dark">
        <h3 class="card-title">
          {{ vaultProp.name }}
        </h3>
        <div class="text-info" v-if="profile.id != vaultProp.creatorId">
          <!--  @click="seeProfilePage(keepProp.creatorId)" -->
          UserIcon here
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
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
        vaultsService.viewVault(props.vaultProp.id)
      }
    }
  }

}
</script>

<style>

</style>
