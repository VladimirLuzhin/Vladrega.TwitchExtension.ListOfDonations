import React, { useState } from 'react'
import { Loader } from '../Loader/Loader'

const State = {
    AwaitSave: 1,
    Loading: 2,
    Success: 3
}

export const Button = ({ onClick, name, title }) => {
    const [state, setState] = useState(State.AwaitSave)

    const onSaveClick = async () => {
        if (state !== State.AwaitSave && state !== State.Error)
            return

        setState(State.Loading)

        const isSaved = await onClick()
        if (isSaved)
            setState(State.Success)
        else
            setState(State.Error)

        setTimeout(() => {
            setState(State.AwaitSave)
        }, 5_000)
    }

    switch (state) {
        case State.AwaitSave:
            return <button title={title} className='active' onClick={onSaveClick}>{name}</button>
        case State.Loading:
            return <button className='active'><Loader /></button>
        default:
            return <button className='success'>✔</button>
    }
}