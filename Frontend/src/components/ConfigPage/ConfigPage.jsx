import React, { useCallback, useRef, useEffect } from 'react'
import { donationRepository } from '../../repository/DonationsRepository'
import { ConfigPresenter } from './ConfigPresenter'

import './Config.css'

export const ConfigPage = () => {
    const channelId = useRef()

    const saveDonationsAsync = useCallback(async (theme, donationsText) => {
        try {
            await donationRepository.saveDonationsAsync(channelId.current, theme, donationsText)
            return true
        }
        catch (e) {
            console.error(e)
            return false
        }
    }, [channelId])

    const deleteAllInformationAsync = useCallback(async () => {
        try {
            await donationRepository.deleteAllInformationAsync(channelId.current)
            return true
        }
        catch (e) {
            console.error(e)
            return false
        }
    }, [channelId])


    useEffect(() => {
        window.Twitch.ext.onAuthorized(function(auth) {
            channelId.current = auth.channelId
        })
    }, [channelId])

    return <div className='config-panel'>
        <ConfigPresenter onSaveAsync={saveDonationsAsync} onDeleteAsync={deleteAllInformationAsync}/>
    </div>
}