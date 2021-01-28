<template>
  <div class="keep">
    <div class="card m-3 bg-light align-text-bottom" data-toggle="modal" :data-target="'#keepModal' + keepProp.id">
      <img :src="keepProp.img" alt="" class="card-img">
      <div class="card-img-overlay row justify-content-between align-items-end text-white">
        <h3 class="card-title px-2">
          {{ keepProp.name }}
        </h3>
        <div class="text-info" v-if="profile.id != keepProp.creatorId">
          <i class="bi bi-person-circle"></i>
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
            <img :src="keepProp.img" alt="" class="img-fluid">
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
                <button class="btn btn-secondary dropdown-toggle btn-sm"
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
              <!-- <img :src="keepProp.Creator.img" alt=""> --><p class="creator">
                <i v-if="profile.id != keepProp.creatoId" class="bi bi-person-circle " @click="viewProfilePage"></i>
                &nbsp;{{ keepProp.creator.name }}
              </p>
              <i class="bi bi-trash-fill" v-if="profile.id == keepProp.creatorId" @click="deleteKeep"></i>
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
            $('#keepModal' + props.keepProp.id).modal('toggle')
            profileService.getKeepsByProfileId(this.profile.id)
          }
        })
      }
    }
  }
}
</script>

<style>
@import url("https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css");
.card-title:hover{
text-decoration: underline;
}
.card img{
  width: 100%;
  transition: 0.3s;
  border-radius: 8px;
}
.card{
  border-radius: 12px;
  background: #fff;
  padding: 1%;
  box-shadow: 10px 10px 24px -12px rgba(0, 0, 0, 0.75);
}
.card:hover{
  box-shadow: inset 10px 10px 24px -12px (0, 0, 0, 0.75);
  transform: scale(0.9);
  transition: 2s ease-in-out;
}
.dropdown button{
  color: #fff;
}
.bi-trash-fill{
  color: #D63031;
}
.creator{
  color: #a655e7;
}
.bi-person-circle{
  color: #a655e7;
}
</style>
