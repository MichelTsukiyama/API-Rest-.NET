import React, {useState} from 'react';
import {useNavigate} from 'react-router-dom';
import api from '../../services/api'

import './styles.css';
import logo from '../../assets/download.png';
import padlock from '../../assets/cadeado(1).png'

export default function Login(){

    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    async function login(e) {
        e.preventDefault();

        const data = {
            userName,
            password,
        };

        try {
            const response = await api.post('Auth/api/v1/signing', data);

            localStorage.setItem('userName', userName);
            localStorage.setItem('accessToken', response.data.accessToken);
            localStorage.setItem('refreshToken', response.data.refreshToken);

            navigate('/books');
        } catch (error) {
            alert('Login failed, try again')
        }
    }

    return (
        <div className="login-container">
            <section className="form">
                <img src={logo} alt="logo" />
                <form onSubmit={login} >
                    <h1>Access your account</h1>

                    <input type="text" placeholder="Username" value={userName} onChange={e => setUserName(e.target.value)}/>

                    <input type="password" placeholder="password" value={password} onChange={e => setPassword(e.target.value)} />

                    <button className="button"type="submit">Login</button>
                </form>
            </section>
            <img src={padlock} alt="login" />
        </div>
    )
}