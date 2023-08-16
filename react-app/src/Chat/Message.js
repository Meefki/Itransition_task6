import React from "react";
import { Tag } from 'antd';

const Message = ({ message, index }) => {
    const formatDate = (fullDate) => {
        const dateTime = fullDate.split('T');

        dateTime[1] = dateTime[1].split('.')[0]

        return dateTime.join(' ');
    }

    return (
        <div className="border-bottom m-2">
            <span className="d-flex justify-content-start">
                <div>
                    {
                        message.tags.map((tag, key) => {
                            return <Tag key={key} className="m-1" color="#55acee">{tag}</Tag>
                        })
                    }
                </div>
                <p>{message.message}</p>
            </span>
            <p>{formatDate(message.sentDate)}</p>
        </div>
    );
}

export default Message;