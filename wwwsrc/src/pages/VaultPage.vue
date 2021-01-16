<template>
  <div class="vault container-fluid">
    <!-- <img class="card img-fluid" :src="vault.img" alt=""> -->
    <div class="row text-center p-4">
      <h2 class="col-11 d-flex justify-content-around">
        {{ vault.name }}
      </h2>
      <i class="bi bi-trash-fill p-2" v-if="profile.id == vault.creatorId" @click="deleteVault(vault.id)"></i>
      <p class="col-11 p-4">
        {{ vault.description }}
      </p>
    </div>
    <br>
    <div class="container">
      <div class="row">
        <div class="col-12">
          <div class="card-columns">
            <keeps-component v-for="k in keeps" :keep-prop="k" :key="k.id" class="card-container mx-2" />
          </div>
        </div>
      </div>
    </div>
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
@import url("https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css");

</style>
