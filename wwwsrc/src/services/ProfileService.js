import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile() {
    try {
      const res = await api.get('api/profiles')
      AppState.profile = res.data
      // logger.log(AppState.profile)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getProfileById(creatorId) {
    try {
      // const res = await api.get('/profile/' + creatorId)
      // AppState.profile = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getKeepsByProfileId(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/keeps')
      AppState.viewingKeeps = res.data
      // logger.log(res.data)
    } catch (error) {
      logger.log(error)
    }
  }

  async getVaultsByProfileId(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      AppState.viewingVaults = res.data
      // logger.log(res.data)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const profileService = new ProfileService()
