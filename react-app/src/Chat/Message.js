import React, { useEffect } from "react";
import { Tag } from 'antd';

function Message({ message, index }) {

    const formatDate = (fullDate) => {
        const dateTime = fullDate.split('T');
        dateTime[1] = dateTime[1].split('.')[0];
        return dateTime;
    }

    return (
        <div className="border-bottom m-2 d-flex justify-content-between">
            <span className="d-flex justify-content-start">
                <div>
                    {
                        message.tags.map((tag, key) => {
                            return <Tag key={key} className="m-1" color="#55acee">{tag}</Tag>
                        })
                    }
                </div>
                <p className="text-wrap">{message.message}</p>
            </span>
            <div className="d-flex justify-content-center justify-content-lg-end justify-content-xl-end align-items-center flex-column flex-lg-row flex-xl-row">
                <Tag className="m-1" color="geekblue">{formatDate(message.sentDate)[0]}</Tag>
                <Tag className="m-1" color="cyan">{formatDate(message.sentDate)[1]}</Tag>
            </div>
        </div>
    );
}

export default Message;