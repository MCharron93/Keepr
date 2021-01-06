<template>
  <div class="keep grid-item m-3">
    <div class="card bg-light align-text-bottom" data-toggle="modal" :data-target="'#keepModal' + keepProp.id">
      <img :src="keepProp.img" alt="" class="card-img">
      <div class="card-img-overlay row justify-content-between align-items-end text-white">
        <h3 class="card-title">
          {{ keepProp.name }}
        </h3>
        <div class="text-info" v-if="profile.id != keepProp.creatorId">
          <i class="bi bi-person-circle"></i>
          <svg xmlns="http://www.w3.org/2000/svg"
               width="16"
               height="16"
               fill="currentColor"
               class="bi bi-person-circle"
               viewBox="0 0 16 16"
          >
            <path d="M13.468 12.37C12.758 11.226 11.195 10 8 10s-4.757 1.225-5.468 2.37A6.987 6.987 0 0 0 8 15a6.987 6.987 0 0 0 5.468-2.63z" />
            <path fill-rule="evenodd" d="M8 9a3 3 0 1 0 0-6 3 3 0 0 0 0 6z" />
            <path fill-rule="evenodd" d="M8 1a7 7 0 1 0 0 14A7 7 0 0 0 8 1zM0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8z" />
          </svg>
        </div>
      </div>
    </div>

    <div class="modal fade"
         :id="'keepModal' + keepProp.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="keepModalTitle"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content row flex-row">
          <div class="align-self-center p-3">
            <img :src="keepProp.img" alt="" class="mx-100 mh-100">
          </div>
          <div class="col p-2 d-flex flex-column flex-grow">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLongTitle">
                {{ keepProp.name }}
              </h5>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <h6 class="align-self-end">
                Kept: {{ keepProp.keeps }}
              </h6>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              {{ keepProp.description }}
            </div>
            <div class="modal-footer flex-row justify-content-between align-items-end">
              <div class="dropdown">
                <button class="btn btn-secondary dropdown-toggle"
                        type="button"
                        id="dropdownMenuButton"
                        data-toggle="dropdown"
                        aria-haspopup="true"
                        aria-expanded="false"
                        v-if="profile.id"
                >
                  &#43; Vault
                </button>
                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                  <button class="dropdown-item" v-for="banana in myVaults" :key="banana.id" @click="addToVault(banana.id)">
                    {{ banana.name }}
                  </button>
                </div>
              </div>
              <!-- <img :src="keepProp.Creator.img" alt=""> -->
              <button class="btn btn-info" @click="viewProfilePage">
                <i class="bi bi-person-circle"></i>
                <svg xmlns="http://www.w3.org/2000/svg"
                     width="16"
                     height="16"
                     fill="currentColor"
                     class="bi bi-person-circle"
                     viewBox="0 0 16 16"
                >
                  <path d="M13.468 12.37C12.758 11.226 11.195 10 8 10s-4.757 1.225-5.468 2.37A6.987 6.987 0 0 0 8 15a6.987 6.987 0 0 0 5.468-2.63z" />
                  <path fill-rule="evenodd" d="M8 9a3 3 0 1 0 0-6 3 3 0 0 0 0 6z" />
                  <path fill-rule="evenodd" d="M8 1a7 7 0 1 0 0 14A7 7 0 0 0 8 1zM0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8z" />
                </svg>
              </button>
              <button class="btn btn-danger" v-if="profile.id == keepProp.creatorId" @click="deleteKeep">
                Delete Keep?
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { profileService } from '../services/ProfileService'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
export default {
  name: 'Keep',
  props: {
    keepProp: Object
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      newVaultKeep: {}
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      keep: computed(() => props.keepProp),
      myVaults: computed(() => AppState.viewingVaults),
      addToVault(vId, kId) {
        // NOTE this should take in the vaultId that it will be assigned to AND the keepId from the keep
        this.state.newVaultKeep = {
          vaultId: vId,
          keepId: props.keepProp.id
        }
        vaultsService.addToVault(this.state.newVaultKeep)
      },
      viewProfilePage() {
        $('#keepModal' + props.keepProp.id).modal('toggle')
        router.push({ name: 'Profile', params: { id: props.keepProp.creatorId } })
        profileService.getProfileById(props.keepProp.creatorId)
      },
      deleteKeep() {
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
            keepsService.deleteKeep(props.keepProp.id)
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
.card-title:hover{
text-decoration: underline;
}
.grid-item {
  float: left;
  width: auto;
  height: auto;
  border: 2px solid hsla(0, 0%, 0%, 0.5);
}
</style>
