import React, { useEffect, useState } from "react";
import Requests from "../Services/Requests";
import Message from "./Message";

const Chat = () => {

    const [messages, setMessages] = useState([]);

    const getMessages = async () => {
        const requests = new Requests();
        const msgs = await requests.getMessages();
        setMessages(msgs ?? []);
    }

    useEffect(() => {
        getMessages();
    }, [])

    return (
        <div className="full-height-container">
            <div className="bg-light border-bottom shadow-sm mb-3">
                <div className="container-fluid" style={{ minHeight: 50 }}>
                    <div className="d-sm-inline-flex justify-content-between">
                    <h2>Chat</h2>
                    </div>
                </div>
            </div>
            <div>
                {
                    messages.sort((m1, m2) => m1.sentDate < m2.sentDate ? -1 : m1.sentDate > m2.sentDate ? 1 : 0).map((message, key) => {
                        return (<Message key={key} message={message} index={key}/>)
                    })
                }
            </div>
        </div>
    )
}

export default Chat;
