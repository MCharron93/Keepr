<template>
  <div class="keep card bg-light text-white align-text-bottom">
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
  </div>
</template>

<script>
import { computed } from 'vue'
import { profileService } from '../services/ProfileService'
import { useRouter } from 'vue-router'
export default {
  name: 'Keep',
  props: {
    keepProp: Object
  },
  setup(props) {
    const router = useRouter()
    return {
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
