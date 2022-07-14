import React from 'react'

import Archangel from '../../images/heroes3/archangel.png'
import Cyclope from '../../images/heroes3/cyclope.png'
import Jhin from '../../images/heroes3/jhin.png'

import Vampire from '../../images/disciples2/vampire.gif'
import GhostWarrior from '../../images/disciples2/ghost-warrior.gif'
import Death from '../../images/disciples2/death.gif'

const themes = new Map([
    [1, {
        themeName: 'heroes3',
        images: new Map([
            [1, Archangel],
            [2, Cyclope],
            [3, Jhin]
        ])
    }],
    [2, {
        themeName: 'disiples2',
        images: new Map([
            [1, Death],
            [2, GhostWarrior],
            [3, Vampire]
        ])
    }]
])

const DonatorAvatar = ({ theme, index }) => {    
    if (!themes.has(theme)) 
        return <>{index}</>
    
    switch (index) {
        case 1:
        case 2:
        case 3:
            return <img className={themes.get(theme).themeName} src={`${themes.get(theme).images.get(index)}`} />
        default:
            return <>{index}</>
    }
}

const Donator = ({ theme, from, amount, index}) => {
    return <div className='donator'>
        <span className='donator-position'>{<DonatorAvatar index={index + 1} theme={theme} />}</span>
        <span className='donator-name' title={from}>{from}</span>
        <span className='donator-amount'>{`${new Intl.NumberFormat('ru-RU').format(amount)}`}</span>
    </div>
}

export const DonatorsList = ({ donators, theme }) => {
    return <div className='donators-list'>
        {donators.length > 0 && donators.map((d, i) => <Donator key={d.from} theme={theme} from={d.from} amount={d.amount} index={i}/>)}
        {donators.length === 0 && <span className='empty-list'>You Haven`t Donations Yet</span>}
    </div>
}