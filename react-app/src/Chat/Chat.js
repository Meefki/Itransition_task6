import React, { useEffect, useState } from "react";
import Message from "./Message";
import Send from '../Resources/send.png'

const styles = {
    messageInput: {
        height: 100
    },
    header: {
        height: 50
    }
}

function Chat({messageTags, sendMessage, messages}) {

    const [messageStr, setMessageStr] = useState('');

    function send() {
        const message = {
            message: messageStr,
            tags: messageTags,
            sentDate: null
        };
        sendMessage(message);
        setMessageStr('');
    }

    return (
        <div className="full-height-container d-flex flex-column border-top">
            <div className="bg-light border-bottom shadow-sm mb-3">
                <div className="container-fluid" style={styles.header}>
                    <h2 className="align-self-center">Chat</h2>
                </div>
            </div>
            <div className="flex-grow-1 overflow-x-hidden overflow-y-auto">
                {
                    messages.sort((m1, m2) => m1.sentDate > m2.sentDate ? 1 : m1.sentDate < m2.sentDate ? -1 : 0).map((message, key) => {
                        return (<Message key={key} message={message} index={key}/>)
                    })
                }
            </div>
            <div className="d-flex flex-column-reverse bg-light border-top shadow-sm mt-3" style={styles.messageInput}>
                <div className="d-flex align-items-center justify-content-center h-100">
                    <div className="d-flex w-100">
                        <input
                            value={messageStr}
                            placeholder="Type message"
                            className="input-group-text w-100 m-2"
                            onChange={(e) => setMessageStr(e.target.value)}/>
                        <button
                            type="submit"
                            className="btn btn-primary m-2 d-flex align-items-center justify-content-center">
                                <p className="m-0" onClick={() => send()}>Send</p>&nbsp;<img src={Send}/>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Chat;
