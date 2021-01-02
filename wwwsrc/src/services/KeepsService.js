import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

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

  async getOneKeep(keepId) {
    try {
      const res = await api.get('/api/keeps/' + keepId)
      logger.log(res.data)
      AppState.oneKeep = res.data
    } catch (error) {
      logger.log(error)
    }
  }
}

export const keepsService = new KeepsService()