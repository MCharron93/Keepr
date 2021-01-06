<template>
  <div class="vault">
    <!-- <img class="card img-fluid" :src="vault.img" alt=""> -->
    <br>
    <h2>
      {{ vault.name }}
    </h2>
    <button v-if="profile.id == vault.creatorId" class="btn btn-danger" @click="deleteVault(vault.id)">
      &times;
    </button>
    <br>
    <keeps-component v-for="k in keeps" :keep-prop="k" :key="k.id" />
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import KeepsComponent from '../components/KeepsComponent.vue'
export default {
  components: { KeepsComponent },
  name: 'Vault',
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(async() => {
      vaultsService.viewVault(route.params.id)
      vaultsService.getKeepsByVaultId(route.params.id)
    })
    return {
      profile: computed(() => AppState.profile),
      vault: computed(() => AppState.oneVault),
      keeps: computed(() => AppState.viewingKeeps),
      deleteVault(vaultId) {
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
            vaultsService.deleteVault(vaultId)
            // eslint-disable-next-line no-undef
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            )
            router.push({ name: 'Home' })
          }
        })
      }
    }
  }
}
</script>

<style>

</style>
