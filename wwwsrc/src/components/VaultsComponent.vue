<template>
  <div class="vaults-component grid-item" @click="viewVault">
    <div class="card bg-light align-text-bottom">
      <img :src="vaultProp.img" alt="" class="card-img">
      <div class="card-img-overlay row justify-content-between align-items-end text-white">
        <h3 class="card-title">
          {{ vaultProp.name }}
        </h3>
        <button v-if="profile.id == vaultProp.creatorId" class="btn btn-danger">
          &times;
        </button>
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
      },
      deleteVault() {
        // eslint-disable-next-line no-undef
        Swal.fire({
          title: 'Are you sure?',
          text: "You won't be able to revert this!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
          if (result.isConfirmed) {
            vaultsService.deleteKeep(props.vaultProp.id)
            // eslint-disable-next-line no-undef
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            )
          }
        })
      }
    }
  }

}
</script>

<style>

</style>
