import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { profileService } from '../services/ProfileService'

class KeepsService {
  async getAllKeeps() {
    try {
      const res = await api.get('/api/keeps')
      AppState.allKeeps = res.data
      // logger.log(res.data)
    } catch (error) {
      logger.log(error)
    }
  }

  async createKeep(keepData, profileId) {
    try {
      await api.post('/api/keeps', keepData)
      profileService.getProfile()
      profileService.getKeepsByProfileId(profileId)
    } catch (error) {
      logger.log(error)
    }
  }

  // async getOneKeep(keepId) {
  //   try {
  //     const res = await api.get('/api/keeps/' + keepId)
  //     AppState.oneKeep = res.data
  //     // logger.log(res.data)
  //   } catch (error) {
  //     logger.log(error)
  //   }
  // }
}

export const keepsService = new KeepsService()
