import React/*, {useState}*/ from 'react';
import Rotas from './routes';
import Login from './pages/Login';
import './global.css';
// import Header from './Header';
// import Login from './pages/Login';

export default function App() {
  //Array[value, changeValueFunction]
  // const [counter, setCounter] = useState(0);


  // function increment(){
  //   setCounter(counter + 1);
  // }
  return (
    // <Header title="Client REST Udemy"/>
    // <Header>
    //   Client REST Udemy
    // </Header>
    // <div>
    //   <Header>
    //     Counter: {counter}
    //   </Header>
    //   <button onClick={increment}>Add</button>
    // </div>
    // <Login></Login>
    <Rotas/>
  );
}

