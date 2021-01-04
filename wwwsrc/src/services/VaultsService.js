import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async viewVault(vaultId) {
    try {
      const res = await api.get('/api/vaults/' + vaultId)
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
}

export const vaultsService = new VaultsService()
