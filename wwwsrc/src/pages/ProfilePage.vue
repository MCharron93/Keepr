<template>
  <div class="profile container-fluid">
    <div class="row text-left d-flex p-5">
      <img class="img-fluid mw-100" :src="profile.picture" alt="" />
      <div class="offset-1">
        <h1>
          {{ profile.name }}
        </h1>
        <h4>
          Vaults: {{ profile.vaults }}
        </h4>
        <h4>
          Keeps: {{ profile.keeps }}
        </h4>
      </div>
    </div>
    <div class="row justify-content-between p-5">
      <!-- NOTE launch modal on click to input form, consider component to loads here that are strictly modals -->
      <h2 class="col-12" data-toggle="modal" data-target="#vaultForm">
        Vaults &#43;
      </h2>
      <vaults-component v-for="v in vaults" :vault-prop="v" :key="v.id" />
    </div>
    <div class="row justify-content-between p-5">
      <!-- NOTE launch modal on click to input form, consider component to loads here that are strictly modals -->
      <h2 class="col-12" data-toggle="modal" data-target="#keepForm">
        Keeps &#43;
      </h2>
      <keeps-component v-for="k in keeps" :key="k.id" :keep-prop="k" class="card-container mx-2" />
    </div>

    <!-- Modal Form for Keeps -->
    <div class="modal fade"
         id="keepForm"
         tabindex="-1"
         role="dialog"
         aria-labelledby="keepFormTitle"
         aria-hidden="true"
    >
      <form @submit.prevent="createKeep" class="form-reset">
        <div class="modal-dialog modal-dialog-centered" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h3>New Keep</h3>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <h6>
                Title:
                <br>
                <input type="text" class="modal-title" id="exampleModalLongTitle" v-model="state.newKeep.name">
              </h6>
              <h6>
                Image URL:
                <br>
                <input type="text" v-model="state.newKeep.img">
              </h6>
              <h6>
                Description:
                <br>
                <textarea cols="60" rows="5" v-model="state.newKeep.description"></textarea>
              </h6>
              <h6>
                Tags:
                <br>
                <input type="text" v-model="state.newKeep.tags">
              </h6>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-dismiss="modal">
                Close
              </button>
              <button class="btn btn-primary" type="submit">
                Save changes
              </button>
            </div>
          </div>
        </div>
      </form>
    </div>

    <!-- Modal for creating Vaults -->
    <div class="modal fade"
         id="vaultForm"
         tabindex="-1"
         role="dialog"
         aria-labelledby="vaultFormTitle"
         aria-hidden="true"
    >
      <form @submit.prevent="createVault" class="vault-form-reset">
        <div class="modal-dialog modal-dialog-centered" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h3>New Vault</h3>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <h6>
                Title:
                <br>
                <input type="text" class="modal-title" id="exampleModalLongTitle" v-model="state.newVault.name">
              </h6>
              <h6>
                Image URL:
                <br>
                <input type="text" v-model="state.newVault.img">
              </h6>
              <h6>
                Description:
                <br>
                <textarea cols="60" rows="5" v-model="state.newVault.description"></textarea>
              </h6>
              <div class="form-check">
                <label class="form-check-label">
                  <input type="checkbox"
                         class="form-check-input"
                         v-model="state.newVault.isPrivate"
                         value="checkedValue"
                         unchecked
                  >
                  Is Private?
                </label>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-dismiss="modal">
                Close
              </button>
              <button class="btn btn-primary" type="submit">
                Save changes
              </button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import KeepsComponent from '../components/KeepsComponent.vue'
import { profileService } from '../services/ProfileService'
import VaultsComponent from '../components/VaultsComponent.vue'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import $ from 'jquery'
export default {
  components: { KeepsComponent, VaultsComponent },
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      newKeep: {},
      newVault: {}
    })
    onMounted(async() => {
      await profileService.getProfileById(route.params.id)
      await profileService.getKeepsByProfileId(AppState.profile.id)
      await profileService.getVaultsByProfileId(AppState.profile.id)
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.viewingKeeps),
      vaults: computed(() => AppState.viewingVaults),
      createKeep() {
        keepsService.createKeep(state.newKeep, AppState.profile.id)
        this.state.newKeep.content = ''
        $('#keepForm').modal('toggle')
      },
      createVault() {
        vaultsService.createVault(state.newVault, AppState.profile.id)
        this.state.newVault.content = ''
        $('#vaultForm').modal('toggle')
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
