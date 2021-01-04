import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile() {
    try {
      const res = await api.get('api/profiles')
      AppState.profile = res.data
      logger.log(AppState.profile)
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
      logger.log(res.data)
      AppState.viewingKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getVaultsByProfileId(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      logger.log(res.data)
      AppState.viewingVaults = res.data
    } catch (error) {
      logger.log(error)
    }
  }
}

export const profileService = new ProfileService()
