<template>
  <div class="keep grid-item">
    <div class="card bg-light align-text-bottom" data-toggle="modal" data-target="#keepModal">
      <img :src="keepProp.img" alt="" class="card-img">
      <div class="card-img-overlay row justify-content-between align-items-end text-white">
        <h3 class="card-title">
          {{ keepProp.name }}
        </h3>
        <div class="text-info" v-if="profile.id != keepProp.creatorId">
          <!--  @click="seeProfilePage(keepProp.creatorId)" -->
          UserIcon here
        </div>
      </div>
    </div>

    <div class="modal fade"
         id="keepModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="keepModalTitle"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-lg " role="document">
        <div class="modal-content row flex-row">
          <div class="col-6 align-self-center">
            <img :src="keepProp.img" alt="" class="img-fluid">
          </div>
          <div class="col-6">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLongTitle">
                {{ keepProp.name }}
              </h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              {{ keepProp.description }}
            </div>
            <div class="modal-footer flex-row justify-content-start">
              <button type="button" class="btn btn-primary" @click="addToVault">
                &#43; Vault
              </button>
              <button class="btn btn-info" @click="viewProfilePage">
                User Icon Here
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { profileService } from '../services/ProfileService'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  name: 'Keep',
  props: {
    keepProp: Object
  },
  newVaultKeep: {
    selected: ''
  },
  setup(props) {
    const router = useRouter()
    return {
      profile: computed(() => AppState.profile),
      keep: computed(() => props.keepProp),
      addToVault() {

      },
      viewProfilePage() {
        router.push({ name: 'Profile', params: { id: props.keepProp.creatorId } })
        profileService.getProfileById(props.keepProp.creatorId)
      }
    }
  }
}
</script>

<style>

</style>
