<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-primary row px-5">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex align-items-end">
        <img
          class="logo"
          alt="logo"
          src="https://cdn1.iconfinder.com/data/icons/andriod-app-logo/32/icon_k-512.png"
          height="45"
        />
        <h4>EEPER</h4>
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse d-flex justify-content-end" id="navbarText">
      <span class="navbar-text">
        <button
          class="btn btn-light text-uppercase text-dark"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <div class="dropdown" v-else>
          <div
            class="dropdown-toggle"
            @click="state.dropOpen = !state.dropOpen"
          >
            <img
              :src="user.picture"
              alt="user photo"
              height="40"
              class="rounded-circle"
            />
            <span class="mx-3">{{ user.name }}</span>
          </div>
          <div
            class="dropdown-menu p-0 list-group w-100"
            :class="{ show: state.dropOpen }"
            @click="state.dropOpen = false"
          >
            <router-link :to="{ name: 'Profile', params: {id: profile.id} }">
              <div class="list-group-item list-group-item-action hoverable">
                Profile
              </div>
            </router-link>
            <div
              class="list-group-item list-group-item-action hoverable"
              @click="logout"
            >
              logout
            </div>
          </div>
        </div>
      </span>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
export default {
  name: 'Navbar',
  setup() {
    const state = reactive({
      dropOpen: false
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        await AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
/* @font-face {
  font-family:'Dosis', sans-serif ;
  src: url("https://fonts.googleapis.com/css2?family=Dosis:wght@800&display=swap");
} */
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
.btn-light:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
</style>
