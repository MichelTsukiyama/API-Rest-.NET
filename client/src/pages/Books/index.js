import React, { useState, useEffect } from "react";
import { Link, Navigate, useNavigate } from "react-router-dom";
import api from "../../services/api";
import { FiPower, FiEdit, FiTrash2 } from "react-icons/fi";
import "./styles.css";
import logo from "../../assets/download.png";

export default function Books() {
  const [books, setBooks] = useState([]);
  const [page, setPages] = useState(1);
  
  const accessToken = localStorage.getItem("accessToken");

  const authorization = {
    headers: {
      Authorization: `Bearer ${accessToken}`
    }
  }

  const userName = localStorage.getItem("userName");

  const navigate = useNavigate();

  useEffect(() => {
    fetchMoreBooks()
  }, [accessToken]);

  async function fetchMoreBooks() {
    const response = await api.get(`api/Book/v1/asc/4/${page}`, authorization);
    setBooks([...books, ...response.data.list]);
    setPages(page + 1);
  }

  async function logout() {
    try {
        await api.get(`Auth/api/v1/revoke`, authorization);

        localStorage.clear();

        navigate('/');

    } catch (error) {
        alert('Logout failed, try it again!')
    }
  }

  async function deleteBook(id) {
    try {
        await api.delete(`api/Book/v1/${id}`, authorization)

        setBooks(books.filter(book => book.id !== id));
    } catch (error) {
        alert('Delete failed. Try again!');
    }
  }

  async function editBook(id) {
    try {
      navigate(`new/${id}`)
    } catch (error) {
        alert('Edit failed! try again!');
    }
  }

  return (
    <div className="book-container">
      <header>
        <img src={logo} alt="logo" />
        <span>
          Welcome, <strong>{userName.toUpperCase()}</strong>!
        </span>
        <Link className="button" to="new/0">
          Add New Book
        </Link>
        <button type="button">
          <FiPower size={18} color="#251fc5" onClick={logout}></FiPower>
        </button>
      </header>

      <h1>Registered Books</h1>
      <ul>
        {books.map((book) => (
          <li key={book.id}>
            <strong>Title:</strong>
            <p>{book.title}</p>
            <strong>Author:</strong>
            <p>{book.author}</p>
            <strong>Price:</strong>
            <p>{Intl.NumberFormat('pt-BR', {style: 'currency', currency: 'BRL'}).format(book.price)}</p>
            <strong>Release Date:</strong>
            <p>{Intl.DateTimeFormat('pt-BR').format(new Date(book.launchDate))}</p>

            <button onClick={() => editBook(book.id)} type="button">
              <FiEdit size={20} color="#251fc5"></FiEdit>
            </button>
            <button onClick={() => deleteBook(book.id)} type="button">
              <FiTrash2 size={20} color="#251fc5"></FiTrash2>
            </button>
          </li>
        ))}
      </ul>
      <button className="button" onClick={fetchMoreBooks} type="button">Load more</button>
    </div>
  );
}
