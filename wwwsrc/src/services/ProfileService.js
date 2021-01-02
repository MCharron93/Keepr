import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile() {
    try {
      const res = await api.get('/profiles')
      AppState.profile = res.data
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
}

export const profileService = new ProfileService()
