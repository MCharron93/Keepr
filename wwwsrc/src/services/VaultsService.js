import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { profileService } from './ProfileService'

class VaultsService {
  async viewVault(id) {
    try {
      const res = await api.get('/api/vaults/' + id)
      AppState.oneVault = res.data
      logger.log(AppState.oneVault)
    } catch (error) {
      logger.log(error)
    }
  }

  async getKeepsByVaultId(vaultId) {
    try {
      const res = await api.get('/api/vaults/' + vaultId + '/keeps')
      logger.log(res.data)
      AppState.viewingKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async createVault(vaultData, profileId) {
    logger.log(vaultData, profileId)
    await api.post('/api/vaults', vaultData)
    profileService.getVaultsByProfileId(profileId)
  }
}

export const vaultsService = new VaultsService()
