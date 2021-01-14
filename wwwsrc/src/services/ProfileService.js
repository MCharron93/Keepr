import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile() {
    try {
      const res = await api.get('api/profiles')
      AppState.profile = res.data
      // logger.log('Are you hitting this?')
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getProfileById(creatorId) {
    try {
      const res = await api.get('/api/profiles/' + creatorId)
      logger.log('Are you hitting this?')
      AppState.profile = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getKeepsByProfileId(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/keeps')
      AppState.viewingKeeps = res.data
      logger.log(AppState.viewingKeeps)
    } catch (error) {
      logger.log(error)
    }
  }

  async getVaultsByProfileId(profileId) {
    try {
      // logger.log(profileId)
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      AppState.viewingVaults = res.data
    } catch (error) {
      logger.log(error)
    }
  }
}

export const profileService = new ProfileService()
