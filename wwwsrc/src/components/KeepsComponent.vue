<template>
  <div class="keep card bg-light text-white align-text-bottom" data-toggle="modal" data-target="#exampleModal">
    <img :src="keepProp.img" alt="" class="card-img">
    <div class="card-img-overlay row justify-content-between align-items-end">
      <h3 class="card-title">
        {{ keepProp.name }}
      </h3>
      <div class="text-info" v-if="profile.id != keepProp.creatorId">
        <!--  @click="seeProfilePage(keepProp.creatorId)" -->
        UserIcon here
      </div>
    </div>
    <div class="modal fade"
         id="exampleModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="exampleModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              Modal title
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            ...
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
            <button type="button" class="btn btn-primary">
              Save changes
            </button>
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
  setup(props) {
    const router = useRouter()
    return {
      profile: computed(() => AppState.profile),
      keep: computed(() => props.keepProp),
      seeProfilePage() {
        router.push({ name: 'Profile', params: { id: props.keepProp.creatorId } })
        profileService.getProfileById(props.keepProp.creatorId)
      }
    }
  }
}
</script>

<style>

</style>
