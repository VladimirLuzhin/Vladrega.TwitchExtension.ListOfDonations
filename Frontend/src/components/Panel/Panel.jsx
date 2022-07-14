import React, { useEffect, useState, useCallback } from "react"
import { DonatorsList } from './Donator'
import { Loader } from '../Loader/Loader'
import { donationRepository } from "../../repository/DonationsRepository"

import './Panel.css'

const PanelState = {
  Loading: 0,
  ShowingInfo: 1,
  Error: 3
}


export const Panel = () => {
  const [state, setState] = useState(PanelState.Loading)
  const [donators, setDonators] = useState([])
  const [theme, setTheme] = useState(0)

  const getDonatorsAsync = useCallback(async (channelId) => {
    try {
      const response = await donationRepository.getDonationsListAsync(channelId)
      setDonators(response.donators)
      setTheme(response.theme)
      setState(PanelState.ShowingInfo)
    }
    catch(e) {
      console.error(e)
      setState(PanelState.Error)
    }
  }, [])

  useEffect(() => {    
    window.Twitch.ext.onAuthorized(async (auth) => {
      await getDonatorsAsync(auth.channelId)
    })
  }, [])

  return <>
    <div className='panel'>
      <div className='header'>👑 Top Donators</div>
      {state === PanelState.Loading && <Loader />}
      {state === PanelState.ShowingInfo && <DonatorsList donators={donators} theme={theme} />}
      {state === PanelState.Error && <div className='error'>Something Going Wrong, Please Try Reloading The Page <br/>💀</div>}
      {state === PanelState.ShowingInfo && donators.length > 0 && <div className='footer'>Thank You For Support ❤️</div>}
    </div>
  </>
}